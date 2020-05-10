var FollowingService = function () {

    var followUnFollow = function(artistId, done, fail) {
        $.post("/api/followings", { artistId })
            .done(done)
            .fail(fail);
    };

    return {
        followUnFollow: followUnFollow
    }

}();