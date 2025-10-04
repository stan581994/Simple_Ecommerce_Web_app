window.dropdownHelper = {
    initialize: function (dotNetHelper, dropdownElement) {
        const clickHandler = function (event) {
            if (dropdownElement && !dropdownElement.contains(event.target)) {
                dotNetHelper.invokeMethodAsync('CloseDropdownFromJS');
            }
        };

        document.addEventListener('click', clickHandler);
        
        // Store the handler so we can remove it later
        dropdownElement._clickHandler = clickHandler;
        dropdownElement._dotNetHelper = dotNetHelper;
    },

    dispose: function (dropdownElement) {
        if (dropdownElement && dropdownElement._clickHandler) {
            document.removeEventListener('click', dropdownElement._clickHandler);
            dropdownElement._clickHandler = null;
            dropdownElement._dotNetHelper = null;
        }
    }
};
