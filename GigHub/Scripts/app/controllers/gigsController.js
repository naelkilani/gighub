﻿var GigsController = function (attendanceService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
    };

    var fail = function () {
        alert("Something failed");
    };

    var done = function () {
        var text = (button.text().trim() == "Going") ? "Going?" : "Going";
        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    };

    return {
        init: init
    }

}(AttendanceService);