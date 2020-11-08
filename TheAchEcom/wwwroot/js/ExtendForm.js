// Create & manage a hidden form
function ExtendForm(formId, method = "get", action) {
    var obj = {
        FormId: formId,
        Method: method,
        Action: action,
        AddAllAvailableField() {
            var fields = $('[extend_form]');
            $(formId).prepend(fields);
        },
        ExtendForm: function () {
            var fields = $(`[extend_form="${this.FormId}"]`);
            $(this.FormId).prepend(fields);
        },
        Submit: function () {
            this.ExtendForm();
            $(this.FormId).submit();
        },
        SubmitSelf: function (element) {
            $(this.FormId).prepend(element);
            $(this.FormId).submit();
        },
        Init: function () {
            // init form
            var _that = this;
            var form = $(_that.FormId)[0];

            if (!form) {
                console.warn(`ExtendForm: No form with ${_that.FormId} found`)
                return;
            }
            $('body').append(form);

            // extend form
            // bind event

            $(`[submit_on_click][extend_form='${_that.FormId}']`).on("click", (e) => {
                _that.Submit()
            });

            $('[submit_on_check]').on("click", (e) => {
                var targetField = $(e.target).attr('submit_on_check');
                $(targetField).prop("checked", true);
                $(targetField).change(() => {
                    _that.Submit();
                });
                $(targetField).trigger("change");
            });

            $('[submit_on_keypress]').keypress(function (e) {
                var val = $(e.target).val();
                if (e.which == 13) {
                    var length = val.length;
                    if ($(e.target).attr('submit_self_only')) {
                        _that.SubmitSelf(e.target);s
                    }
                    else {
                        _that.Submit();
                    }
                }
            });
        }
    };

    obj.Init();
    return obj;
}
