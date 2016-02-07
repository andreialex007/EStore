var UIToastr = function () {

    return {
        ShowMessage: function(type, msgTitle, message) {
            var i = -1,
                toastCount = 0,
                $toastlast;

            var shortCutFunction = type;
            var msg = message;
            var title = msgTitle || '';
            var $showDuration = '1000';
            var $hideDuration = '1000';
            var $timeOut = '5000';
            var $extendedTimeOut = '5000';
            var $showEasing = 'swing';
            var $hideEasing = 'linear';
            var $showMethod = 'fadeIn';
            var $hideMethod = 'fadeOut';
            var toastIndex = toastCount++;


            toastr.options = {
                closeButton: true,
                debug: false,
                positionClass:'toast-top-right',
                onclick: null
            };
         
            if ($showDuration.length) {
                toastr.options.showDuration = $showDuration;
            }

            if ($hideDuration.length) {
               toastr.options.hideDuration = $hideDuration;
               
            }

            if ($timeOut.length) {
                if (shortCutFunction == 'error') {
                    toastr.options.timeOut = '10000000000'; // We want error box does not close by it self.

                } else
                    toastr.options.timeOut = $timeOut;
            }

            if ($extendedTimeOut.length) {
                toastr.options.extendedTimeOut = $extendedTimeOut;
            }

            if ($showEasing.length) {
                toastr.options.showEasing = $showEasing;
            }

            if ($hideEasing.length) {
                toastr.options.hideEasing = $hideEasing;
            }

            if ($showMethod.length) {
                toastr.options.showMethod = $showMethod;
            }

            if ($hideMethod.length) {
                toastr.options.hideMethod = $hideMethod;
            }

            var $toast = toastr[shortCutFunction](msg, title); // Wire up an event handler to a button in the toast, if it exists
            $toastlast = $toast;
            if ($toast.find('#okBtn').length) {
                $toast.delegate('#okBtn', 'click', function () {
                    alert('you clicked me. i was toast #' + toastIndex + '. goodbye!');
                    $toast.remove();
                });
            }
            if ($toast.find('#surpriseBtn').length) {
                $toast.delegate('#surpriseBtn', 'click', function () {
                    alert('Surprise! you clicked me. i was toast #' + toastIndex + '. You could perform an action here.');
                });
            }
        }

    };

}();