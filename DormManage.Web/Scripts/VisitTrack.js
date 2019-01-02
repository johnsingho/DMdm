/**
* sysName -- 系统名字，必需
* modName -- 模块名字，可选
* loginUser -- 登录用户名，可选
*/
function VisitTrack(sysName, modName, loginUser) {
    var sUrl = "http://dmnnt022:9900/VisitTrackService.svc/VisitTrackWeb/Visit";
    var paras = {
        'sysname': sysName,
        'modname': modName,
        'loginuser': loginUser
    };
    $.ajax({
        url: sUrl,
        data: paras,
        dataType: "jsonp",
        error: function (xhr, status, error) {
            console.log("Error:" + error);
        }
    });
}
