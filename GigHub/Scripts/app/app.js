var GigsController = function(attendanceService) {
    var button;

    var init = function() {
        $(".js-toggle-attendance").click(toggleAttendance);
    };

    var toggleAttendance = function(e) {
        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
    };

    var fail = function() {
        alert("Something failed");
    };

    var done = function() {
        var text = button.text() == "Going" ? "Going?" : "Going";
        button.toggleClass("btn-default").toggleClass("btn-primary").text(text);
    };

    return {
        init: init
    }

}(AttendanceService);

var AttendanceService = function() {

    var createAttendance = function(gigId, done, fail) {
        $.post("/api/attendances", { gigId: gigId })
            .done(done)
            .fail(fail);
    };

    var deleteAttendance = function (gigId, done, fail) {
        $.ajax({
                url: "/api/attendances/" + gigId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    };

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();