(function ($) {
    $.extend({
        bb_floating_toggle: function (id, anchor, floating, option) {
            //移除自动浮动监听
            $.bb_floating_cleanup(id);

            //获取锚点元素
            let reference, indicator = null;
            switch (option.category) {
                case 1:
                case 3:
                    reference = document.getElementById(anchor);
                    break;
                case 2:
                    reference = anchor;
                    break;
                case 4:
                    reference = document.querySelector(anchor);
                    break;
                case 5:
                    reference = {
                        getBoundingClientRect() {
                            return {
                                width: anchor.width,
                                height: anchor.height,
                                x: anchor.left,
                                y: anchor.top,
                                top: anchor.top,
                                left: anchor.left,
                                right: anchor.left,
                                bottom: anchor.top,
                            };
                        },
                    };
                    break;
            }

            if (reference == null) {
                console.error("Reference not found.");
                return;
            }

            //配置中间件
            let config = { strategy: option.position, placement: option.placement, middleware: [] };

            //设置偏移量
            if (option.axisOffset) {
                config.middleware.push(FloatingUIDOM.offset(option.axisOffset));
            }

            //设置自动翻动来保持可见
            if (option.useFlip) {
                config.middleware.push(FloatingUIDOM.flip());
            }

            //设置保持内容完整显示
            if (option.shiftPadding) {
                config.middleware.push(FloatingUIDOM.shift({ padding: option.shiftPadding }));
            }

            //设置指示箭头
            if (option.useArrow) {
                indicator = floating.querySelector('[data-element=arrow]');
                if (indicator != null) {
                    config.middleware.push(FloatingUIDOM.arrow({ element: indicator }));
                }
            }

            //设置自动隐藏
            if (option.autoHide) {
                config.middleware.push(FloatingUIDOM.hide());
            }

            //控制显示状态
            if (option.visible) {
                if (floating.style.display == 'none') {
                    Object.assign(floating.style, {
                        display: 'block',
                        position: option.strategy,
                        left: null,
                        top: null,
                        opacity: 0,
                        transform: `scale(0)`,
                        transition: 'transform 0.2s ease, opacity 0.1s ease',
                        'z-index': -1000,
                    });
                }
            } else {
                if (floating.style.display != 'none') {
                    Object.assign(floating.style, { display: 'none' });
                }
                return;
            }

            //定义更新方法
            async function update() {
                //隐藏时不计算
                if (floating.style.display == 'none') {
                    return;
                }

                await FloatingUIDOM.computePosition(reference, floating, config)
                    .then(({ x, y, placement, strategy, middlewareData }) => {
                        const left = `${Math.round(x)}px`;
                        const top = `${Math.round(y)}px`;

                        if (floating.style.left == left && floating.style.top == top) {
                            return;
                        }

                        if (indicator != null) {
                            const p = placement.split('-')[0];
                            const side = { top: 'bottom', right: 'left', bottom: 'top', left: 'right' }[p] ?? 'bottom';
                            const offset = `${option.arrowOffset}px`;
                            let left1 = '', top1 = '';
                            if (middlewareData.arrow != undefined) {
                                if (middlewareData.arrow.x != undefined) {
                                    left1 = `${Math.round(middlewareData.arrow.x)}px`;
                                }
                                if (middlewareData.arrow.y != undefined) {
                                    top1 = `${Math.round(middlewareData.arrow.y)}px`;
                                }
                            }

                            Object.assign(indicator.style, { position: strategy, top: top1, left: left1, [side]: offset });
                        }

                        Object.assign(floating.style, {
                            position: strategy,
                            left: left,
                            top: top,
                            opacity: 1,
                            transform: `scale(1)`,
                            transition: 'transform 0.2s ease, opacity 0.1s ease',
                            'z-index': null,
                        });

                        if (option.autoHide) {
                            Object.assign(floating.style, {
                                visibility: middlewareData.hide.referenceHidden ? 'hidden' : 'visible'
                            });
                        }
                    });
            }

            //第一次需要手动调用
            update();

            //注册自动更新
            if (option.autoUpdate && option.category != 5) {
                window.floatings.set(id, FloatingUIDOM.autoUpdate(reference, floating, update));
            }
        },
        bb_floating_cleanup: function (id) {
            if (!window.floatings) {
                window.floatings = new Map();
            } else if (window.floatings.has(id)) {
                window.floatings.get(id).call(globalThis);
                window.floatings.delete(id);
            }
        }
    });
})(jQuery);
