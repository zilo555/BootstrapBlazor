import { offset, flip, shift, arrow, computePosition, autoUpdate } from '@floating-ui/dom';

const cleanups: Map<string, Function> = new Map<string, Function>();

//创建浮动层
export function createFloating(interop: any, floating: HTMLElement, target?: HTMLElement, config?: any) {
    //直接js控制后，再让组件diff，blazor只渲染一次，延迟低
    const _reference = target || document.querySelector(config.targetSelecter);
    if (!interop || !floating || !config || !_reference) {
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
    let _arrow: HTMLElement | null, _config = {
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
        _arrow && _config.middleware.push(arrow({ element: _arrow }));
    }

    //如果初始不显示，需要设置可见
    config.hidden && Object.assign(floating.style, { display: 'block', opacity: 0, position: config.strategy, 'z- index': -1000 });

    //定义更新位置方法
    async function update() {
        await computePosition(_reference, floating, _config)
            .then(({ x, y, placement, strategy, middlewareData }) => {
                //去除重复渲染
                if (floating.style.left != x.toString() || floating.style.top != y.toString()) {
                    if (_arrow) {
                        const arrowX: Number | undefined = middlewareData.arrow?.x;
                        const arrowY: Number | undefined = middlewareData.arrow?.y;
                        const arrowOffset: string = { top: 'bottom', right: 'left', bottom: 'top', left: 'right' }[placement.split('-')[0]] ?? 'bottom';
                        const top = arrowY == null ? '' : `${arrowY}px`;
                        const left = arrowX == null ? '' : `${arrowX}px`;
                        Object.assign(floating.style, { position: strategy, left: `${x}px`, top: `${y}px`, });
                        Object.assign(_arrow.style, { position: strategy, left: left, top: top, [arrowOffset]: '-4px' });
                        interop.invokeMethodAsync('ApplyStyles', x, y, arrowX, arrowY, arrowOffset);
                    } else {
                        Object.assign(floating.style, { position: strategy, left: `${x}px`, top: `${y}px`, });
                        interop.invokeMethodAsync('ApplyStyles', x, y, null, null, null);
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
