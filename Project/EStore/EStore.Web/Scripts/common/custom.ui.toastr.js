var UIToastr = function () {

    return {
        ShowMessage: function(type, msgTitle, message, closeButton) {
            var i = -1,
                toastCount = 0,
                $toastlast;

            var $closeButton = typeof closeButton !== 'undefined' ? closeButton : true;

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
                closeButton: $closeButton,
                debug: false,
                positionClass: 'toast-top-center',
                onclick: null
            };
         
            if ($showDuration.length) {
                toastr.options.showDuration = $showDuration;
            }

            if ($hideDuration.length) {
               toastr.options.hideDuration = $hideDuration;
               
            }

            if ($extendedTimeOut.length) {
                toastr.options.extendedTimeOut = $extendedTimeOut;
            }

            if ($timeOut.length) {
                if (shortCutFunction == 'error' || shortCutFunction =='info') {
                    toastr.options.timeOut = '0';
                    toastr.options.extendedTimeOut = '0'; // We want error box does not close by it self.
                } else
                    toastr.options.timeOut = $timeOut;
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