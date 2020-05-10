var GigDetailsController = function(followingService) {
    var button;

    var init = function(container) {
        $(container).on("click", ".js-toggle-following", toggleFollowing);
    }

    var toggleFollowing = function (e) {
        button = $(e.target);

        followingService.followUnFollow(button.attr("data-artist-id"), done, fail);
    }

    var done = function () {
        var text = (button.text().trim() == "Following") ? "Follow" : "Following";
        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    }

    var fail = function() {
        alert("An error has occured!");
    }

    return {
        init: init
    }

}(FollowingService);