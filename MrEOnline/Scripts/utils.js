var messageType = {
    Unknown: 0,
    Error: 1,
    Success: 2,
    Warning: 3
};

function extract(rows) {
    var stakeholderId = $("#sensitivity-matter").data("stakeholderid");
    var responses = [];

    rows.each(function (index) {
        var matterOfSensitivityId = $(this).data("matterofsensivityid");
        var responseType = $(this).data("responsetype");
        var controls = $(this).find("select,input[type='text']");
        var rOptionId = undefined;
        var qResponse = undefined;
        var stakeholderMatterOfSensitivityId = parseInt($(this).data("key"));

        var object = {
            MatterOfSensitivityId: parseInt(matterOfSensitivityId),
            StakeholderId: stakeholderId
        };

        if (stakeholderMatterOfSensitivityId > 0)
            object.StakeholderMattersOfSensitivityId = stakeholderMatterOfSensitivityId;

        $(this).find("select,input[type='text']").each(function (index, elem) {
            if ($(elem).is("select")) {
                rOptionId = $(elem).find("option:selected").val();
                if (rOptionId != "") {
                    object.ReponseOptionId = parseInt(rOptionId);
                    var textArea = $(elem).parent().find("textarea");
                    if (textArea) {
                        object.ResponseText = $(textArea).val();
                    }
                    responses.push(object);
                }
            } else if ($(elem).is("input")) {
                if ($(elem).val() != "") {
                    object.ResponseText = $(elem).val();
                    responses.push(object);
                }
            }
        });
    });
    return responses;
}

function extractCommittee(rows) {
    var committeeId = $("#sensitivity-matter").data("committeeid");
    var responses = [];

    rows.each(function (index) {
        var matterOfSensitivityId = $(this).data("matterofsensivityid");
        var responseType = $(this).data("responsetype");
        var controls = $(this).find("select,input[type='text']");
        var rOptionId = undefined;
        var qResponse = undefined;
        var committeeMatterOfSensitivityId = parseInt($(this).data("key"));

        var object = {
            MatterOfSensitivityId: parseInt(matterOfSensitivityId),
            CommitteeId: committeeId
        };

        if (committeeMatterOfSensitivityId > 0)
            object.CommitteeMattersOfSensitivityId = committeeMatterOfSensitivityId;

        $(this).find("select,input[type='text']").each(function (index, elem) {
            if ($(elem).is("select")) {
                rOptionId = $(elem).find("option:selected").val();
                if (rOptionId != "") {
                    object.ReponseOptionId = parseInt(rOptionId);
                    var textArea = $(elem).parent().find("textarea");
                    if (textArea) {
                        object.ResponseText = $(textArea).val();
                    }
                    responses.push(object);
                }
            } else if ($(elem).is("input")) {
                if ($(elem).val() != "") {
                    object.ResponseText = $(elem).val();
                    responses.push(object);
                }
            }
        });
    });
    return responses;
}

function isValid(rows) {
    var errorCounter = 0
    var controls = rows.find("select, input[type='text']");
    reset(controls);

    controls.each(function (index) {  
        var option = undefined;
        if ($(this).is("select")) {
            if ((option = $(this).find("option:selected").val()) == "") {
                $(this).addClass("validation-highlight");
                errorCounter++;
            } else {
                var parentRow = $(this).parents("tr").filter("[data-responsetype='Hybrid']");
                if (parentRow && $(this).find("option:selected").text() === 'No') {
                    var textArea = $(parentRow).find("textarea");
                    if (textArea && $(textArea).val() === "") {
                        $(textArea).addClass("validation-highlight");
                        errorCounter++;
                    }
                }    
            }
        } else if ($(this).is("input") && $(this).val() == "") {
            $(this).addClass("validation-highlight");
            errorCounter++;
        }
    });

    if (errorCounter > 0)
        showFeedback("Validation errors have been found and highlighted in red. Please fix.", messageType.Error);
    else
        hideFeedback();

    return errorCounter == 0
}

function reset(controls) {
    controls.each(function (index) {
        $(this).removeClass("validation-highlight");
    })
}

function getUrlBasePathName() {
    if ($.trim(window.location.hostname) != "localhost") {
        var splitPathName = window.location.pathname.split("/");
        return ((splitPathName != undefined || splitPathName.length > 2) ? ("/" + splitPathName[1]) : "");
    }
    return "";
}

function showFeedback(message, type) {
    var target = undefined;
    var snacks = undefined;

    if ((snacks = $('.snackbar')).length > 1)
        $(snacks[0]).remove();

    switch (type) {
        case messageType.Error:
            (target = $('.alert-danger')).removeClass("hidden");
            $('.alert-warning, .alert-success').removeClass("snackbar").addClass("hidden");
            break;
        case messageType.Warning:
            (target = $(".alert-warning")).removeClass("hidden");
            $('.alert-danger, .alert-success').removeClass("snackbar").addClass("hidden");
            break;
        case messageType.Success:
            (target = $(".alert-success")).removeClass("hidden");
            $('.alert-danger, .alert-warning').removeClass("snackbar").addClass("hidden");
            break;
        default:
            break;
    }

    $(target).find(".message").empty().append(message);
    $(target).addClass("snackbar");
    showSnack(20000);
}

function hideFeedback() {
    if (!$(".feedback").hasClass("hidden"))
        $(".feedback").addClass("hidden");
}

function scrollToTop() {
    verticalOffset = typeof (verticalOffset) != 'undefined' ? verticalOffset : 0;
    element = $('body');
    offset = element.offset();
    offsetTop = offset.top;
    $('html, body').animate({ scrollTop: offsetTop }, 500, 'linear');
}

function showSnack(duration) {
    $(".snackbar").addClass("show");
    setTimeout(function () {
        $(".snackbar").removeClass("show");
    }, duration);  
}

function initTracking(selectElement) {
    var stakeholderId = $(selectElement).find("option:selected").val();

    if (stakeholderId !== "")
        $.get(getUrlBasePathName() + "/StakeholderRelationshipAssessments/GetByStakeholder?stakeholderId=" + stakeholderId)
            .done(function (response) {
                if (response.Tracking) {
                    $(".tracking").removeClass("hidden");
                    $(".tracking").find("input, textarea").each(function (index, elem) {
                        $(this).attr("required", true);
                    });
                } else {
                    if (!$(".tracking").hasClass("hidden"))
                        $(".tracking").addClass("hidden");

                    $(".tracking").find("input, textarea").each(function (index, elem) {
                        $(this).val("");
                        $(this).attr("required", false);
                    });
                }
            });
}

function initTrackingCommittees(selectElement) {
    var committeeId = $(selectElement).find("option:selected").val();

    if (committeeId !== "")
        $.get(getUrlBasePathName() + "/CommitteeRelationshipAssessments/GetByCommittee?committeeId=" + committeeId)
            .done(function (response) {
                if (response.Tracking) {
                    $(".tracking").removeClass("hidden");
                    $(".tracking").find("input, textarea").each(function (index, elem) {
                        $(this).attr("required", true);
                    });
                } else {
                    if (!$(".tracking").hasClass("hidden"))
                        $(".tracking").addClass("hidden");

                    $(".tracking").find("input, textarea").each(function (index, elem) {
                        $(this).val("");
                        $(this).attr("required", false);
                    });
                }
            });
}