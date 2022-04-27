export function bb_markdown(el, value, method, obj) {
    // 自动加载样式
    BootstrapBlazorModules.addLink('_content/BootstrapBlazor.Markdown/css/bootstrap.blazor.markdown.min.css');

    var $el = $(el);
    if (method === "setMarkdown") {
        var editor = $.data(el, 'editor');
        editor.setMarkdown(value);
    }
    else {

        // 修复弹窗内初始化值不正确问题
        var handler = window.setInterval(function () {
            if ($el.is(':visible')) {
                window.clearInterval(handler);
                value.el = el;
                value.plugins = [];
                if (value.enableHighlight) {
                    value.plugins.push(toastui.Editor.plugin.codeSyntaxHighlight);
                }
                delete value.enableHighlight;
                const editor = toastui.Editor.factory(value);
                $.data(el, 'editor', editor);
                var timer;
                editor.on('change',
                    function() {
                        // 清空计时器的方法
                        clearTimeout(timer);

                        // 创建一个计时器，开始倒计时，倒计时结束后执行内部的方法
                        timer = setTimeout(function() {
                                // 清除计时器，使下次事件不能进入到if中
                                timer = null;
                                var val = editor.getMarkdown();
                                var html = editor.getHTML();
                                obj.invokeMethodAsync(method, [val, html]);
                            },
                            100);
                    });
            }
        }, 100);
    }
};

export function bb_markdown_method(el, method, parameter, obj) {
    var editor = $.data(el, 'editor');
    if (editor) {
        editor[method](...parameter);
        var val = editor.getMarkdown();
        var html = editor.getHTML();
        obj.invokeMethodAsync('Update', [val, html]);
    }
}
