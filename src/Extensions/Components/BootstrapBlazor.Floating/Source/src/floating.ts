import { ReferenceElement, Placement, Strategy } from '@floating-ui/dom';
import { offset, flip, shift, arrow, computePosition, autoUpdate} from '@floating-ui/dom';

const cleanups: Map<string, () => void> = new Map<string, () => void>();

declare type FloatingRefType = 'cssSelecter' | 'elementReference' | 'idComponentBase' | 'mouseEventArgs';

interface FloatingConfig {
    show: boolean;
    referenceType: FloatingRefType;
    initialState: string;
    strategy: Strategy;
    placement: Placement;
    mainAxis: number;
    shiftPadding: number;
    useArrow: boolean;
    arrowOffset: number;
    autoUpdate: boolean;
    refElementId: string | null;
}

//创建浮动层
export function computeFloating(interop: any, reference: any, floating: HTMLElement, config: FloatingConfig) {
    //直接js控制后，再让组件diff，blazor只渲染一次，延迟低

    let _arrow: HTMLElement | null = null;
    let _reference: ReferenceElement | null = null;
    switch (config.referenceType) {
        case 'cssSelecter': _reference = document.querySelector(reference); break;
        case 'elementReference': _reference = reference; break;
        case 'idComponentBase': _reference = document.getElementById(config.refElementId!)!; break;
        case 'mouseEventArgs': _reference = {
            getBoundingClientRect() {
                return {
                    width: 0,
                    height: 0,
                    x: reference.ClientX,
                    y: reference.ClientY,
                    top: reference.ClientY,
                    left: reference.ClientX,
                    right: reference.ClientX,
                    bottom: reference.ClientY,
                };
            },
        }; break;
    };

    if (!interop || !_reference) {
        return;
    }

    //清除历史浮动配置
    const id = floating.dataset.id;
    if (config?.autoUpdate == true) {
        if (id && cleanups.has(id)) {
            cleanups.get(id)?.call(globalThis);
            cleanups.delete(id);
        }
    }

    //配置中间件
    let _config: any = {
        strategy: config.strategy,
        placement: config.placement,
        middleware: [
            //设置偏移量
            offset(config.mainAxis),
            //设置自动翻动来保持可见
            flip(),
            //设置保持内容完整显示
            shift({ padding: config.shiftPadding }),
        ]
    };

    //设置指示箭头
    if (config.useArrow) {
        _arrow = floating.querySelector<HTMLElement>('[data-element=arrow]');
        if (_arrow != null) {
            _config.middleware.push(arrow({ element: _arrow }));
        }
    }

    //如果初始不显示，需要设置可见
    if (config.initialState == "hidden" && config.show == false) {
        Object.assign(floating.style, {
            display: 'block',
            position: config.strategy,
            left: null,
            top: null,
            opacity: 0,
            transform: `scale(0)`,
            transition: 'transform 0.2s ease, opacity 0.1s ease',
            'z-index': -1000,
        });
    }

    //定义更新位置方法
    async function update() {
        await computePosition(_reference!, floating, _config)
            .then(({ x, y, placement, strategy, middlewareData }) => {
                console.info(x + '|' + y + '|' + floating.style.left + '|' + floating.style.top);

                //去除重复渲染
                if (floating.style.left != x.toString() || floating.style.top != y.toString()) {
                    if (_arrow == null) {
                        Object.assign(floating.style, {
                            position: strategy,
                            left: `${x}px`,
                            top: `${y}px`,
                            opacity: 1,
                            transform: `scale(1)`,
                            transition: 'transform 0.2s ease, opacity 0.1s ease',
                            'z-index': null,
                        });
                        interop.invokeMethodAsync('ApplyStyles', x, y, null, null, null);
                    } else {
                        const x1: Number | undefined = middlewareData.arrow?.x;
                        const y1: Number | undefined = middlewareData.arrow?.y;
                        const top = y1 == undefined ? '' : `${y1}px`;
                        const left = x1 == undefined ? '' : `${x1}px`;
                        const side: string = {
                            top: 'bottom',
                            right: 'left',
                            bottom: 'top',
                            left: 'right'
                        }[placement.split('-')[0]] ?? 'bottom';

                        Object.assign(floating.style, {
                            position: strategy,
                            left: `${x}px`,
                            top: `${y}px`,
                            opacity: 1,
                            transform: `scale(1)`,
                            transition: 'transform 0.2s ease, opacity 0.1s ease',
                            'z-index': null,
                        });

                        Object.assign(_arrow.style, {
                            position: strategy,
                            left: left,
                            top: top,
                            [side]: `${config.arrowOffset}px`
                        });

                        interop.invokeMethodAsync('ApplyStyles', x, y, x1, y1, side);
                    }
                }
            });
    }

    //第一次需要手动调用
    update();

    //注册自动更新
    if (config?.autoUpdate == true) {
        id && cleanups.set(id, autoUpdate(_reference, floating, update));
    }
}

///清除浮动层
export function cleanupFloating(floating: HTMLElement) {
    const id = floating.dataset.id;
    if (id && cleanups.has(id)) {
        cleanups.get(id)?.call(globalThis);
        cleanups.delete(id);
    }
}
