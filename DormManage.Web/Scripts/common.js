// JavaScript Document
// -------------------
// -------------------
// JavaScript Document

function CheckAll(tempControl) {
    //将除头模板中的其它所有的CheckBox取反
    var theBox = tempControl;
    xState = theBox.checked;
    //elem = theBox.form.elements;
    test = tempControl.parentElement.parentElement.parentElement;
    elem = test.getElementsByTagName("input")
    for (i = 0; i < elem.length; i++)
        if (elem[i].type == 'checkbox' && elem[i].id != theBox.id) {
            if (elem[i].checked != xState)
                elem[i].click();
        }
}

$(function () {
    $("#tree>div").hide();
    $("#tree>div:first").show();
    $("#tree>h2:first").addClass("on");
    $("#tree>h2").click(function () {
        if ($(this).attr('class') != 'on') {
            $(this).addClass("on");
            $(this).next().slideToggle(200);
        } else {
            $(this).removeClass("on");
            $(this).next().slideToggle(200);
        }
    });
    $.each($("#tree>div>ul>li"), function () {
        $(this).click(function () {
            $("#tree>div>ul>li").removeClass("on");
            $(this).addClass("on");
        });
    });

    /*调整wrapper样式*/
    $(".wrapper").append("<div style = 'height:1px; margin-top:-1px;clear: both;overflow:hidden;'></div>");
    /*form*/
    $('.viewlist table tr').find('td:first').addClass('alignleft');
    $('.preview .viewlist table tr').find('td:first').addClass('aligncenter');
    $('.tabbox .viewlist table tr').find('td:first').addClass('aligncenter');
    $('.datapeer .viewlist table tr').find('td:first').addClass('aligncenter');
    $('.viewlist table tr:odd').css({ 'background': '#fff' });
    $('.viewlist table tr:even').css({ 'background': '#fff' });

    $('.viewlist table tr:odd').find("td").find("table").find("tr").css({ 'background': '#fff' });
    $('.viewlist table tr:even').find("td").find("table").find("tr").css({ 'background': '#fff' });


    $('.viewlist table tr').hover(function () {
        $(this).css({ 'background': '#f0f8fd' }); //#3c96d4
        $($(this).find("td")).css("color", "#000");
        $(this).find("td").find("table").find("tr").find("td").css("background", "#f0f8fd");//#3c96d4
    }, function () {
        $('.viewlist table tr:odd').css({ 'background': '#fff' });//#f9f9f9
        $('.viewlist table tr:even').css({ 'background': '#fff' });
        $($(this).find("td")).css("color", "#000");
        $(this).find("td").find("table").find("tr").find("td").css("background", "#fff");//#f9f9f9
        $('.viewlist table tr:odd').find("td").find("table").find("tr").css({ 'background': '#fff' });//#f4f4f5
        $('.viewlist table tr:even').find("td").find("table").find("tr").css({ 'background': '#fff' });
    });

    $('.viewlist table tr').find("td").find("table").find("tr").unbind("mouseout");

    $('a').click(function () {
        $(this).blur();
    });
    $('a').focus(function () {
        $(this).blur();
    });

    //折叠菜单栏控件按钮
    $("#mid").toggle(function () {
        $("#tree_nav").hide();
        $(this).html("<input type='image' title='展开' class='m'/>");
        $("#frm").width($(window).width() - 9);
    }, function () {
        $("#tree_nav").show();
        $(this).html("<input type='image' title='关闭'/>");
        $("#frm").width($(window).width() - 209);
    });

    //登录页输入框获取焦点
    $('#text3').focus(function () {
        $('#choose_btn').removeClass('choose_btn');
        $('#choose_btn').addClass('choose_btn2');
    });

    $('#text3').blur(function () {
        $('#choose_btn').removeClass('choose_btn2');
        $('#choose_btn').addClass('choose_btn');
    });

    $('#choose_btn').click(function () {
        $('#text3').focus();
    })

    //标签切换
    $('.tab a').click(function () {
        $('.tab a').removeClass('focus');
        $(this).addClass('focus');

        var index = $('.tab a').index($(this));
        $('.tabbox').hide();
        $('.tabbox').eq(index).show();
    });

    //内标签切换
    $('.insidetab a').click(function () {
        $('.insidetab a').removeClass('focus');
        $(this).addClass('focus');

        var index = $('.insidetab a').index($(this));
        $('.preview').hide();
        $('.preview').eq(index).show();
    });

    //内树菜单高度
    resize();

    //数据互传列表查询条件框的显示和隐藏
    $('.datapeer input.togglebar').toggle(function () {
        $(this).parent().next('div').hide();
        $(this).parent().find('input').addClass('togglebar_down');
    }, function () {
        $(this).parent().next('div').show();
        $(this).parent().find('input').removeClass('togglebar_down');
    });

    //首页显示隐藏
    $('.wrapper .title .togbar').toggle(function () {
        $(this).parent().next('div').hide();
        $(this).html('<input type="img" class="changebar_down" />');
    }, function () {
        $(this).parent().next('div').show();
        $(this).html('<input type="img" class="changebar" />');
    })

    //内树菜单收起
    $('.innertree h5 a').click(function () {
        $(this).parent().next().slideToggle();
    })


});

//内树菜单高度
function resize() {

    var h = $(window).height() - 26;
    $('.innertree').height(h);
}
function treeresize() {
    var h2 = $(window).height() - 96;
    var h3 = $(document).height() - 96;
    if ($('#tree').height() < h2) {
        $('#tree').height(h2);
    }
}
//登录页
function changeheight() {
    $('ul#pro1').show();
    $("#login_left").height($(window).height() - 150);
    $("#login_right2").height($(window).height() - 391);

    $("#login_right2").height($(window).height() - 391);
    var pro = $("#login_right2").height();

    if (pro > 617) {
        $('ul#pro1').show();
        $('ul#pro2').show();
        $('ul#pro3').show();
        $('ul#pro4').show();
    } else if (pro > 471) {
        $('ul#pro1').show();
        $('ul#pro2').show();
        $('ul#pro3').show();
    } else if (pro > 325) {
        $('ul#pro1').show();
        $('ul#pro2').show();
    } else {
        $('ul#pro1').show();
    }
}

function changewidth() {
    var r = document.getElementById("frm");
    var d = document.getElementById("tree");
    //var t = document.getElementById("tree2");
    //try{
    //	if(t.style.display == 'none'){
    //		var bwidth = (document.body.clientWidth - 11) +"px";
    //		r.style.width =  bwidth;
    //	}else{
    //		var bwidth = (document.body.clientWidth - 211) +"px";
    //		r.style.width =  bwidth;
    //	}
    //}catch (ex){}
    //footer固定
    if (r.height > (document.body.clientHeight - 100)) {
        var f = document.getElementById("footer");
        f.className = "f_static";
    } else if (d.offsetHeight > (document.body.clientHeight - 100)) {
        var f = document.getElementById("footer");
        f.className = "f_static";
    } else if (d.offsetHeight < (document.body.clientHeight - 100)) {
        var f = document.getElementById("footer");
        f.className = "footer";
    } else {
        var f = document.getElementById("footer");
        f.className = "footer";
    }
}
//iframe高度自适应
function reinitIframe() {
    var iframe = document.getElementById("frm");
    try {
        var kHeight = frames['frm'].document.body.scrollHeight;
        var pHeight = frames['frm'].document.documentElement.scrollHeight;
        var bHeight = iframe.contentWindow.document.body.clientHeight;
        var dHeight = iframe.contentWindow.document.documentElement.clientHeight;
        var height = Math.max(bHeight, dHeight);
        iframe.height = height;
    } catch (ex) { }
}
window.setInterval("reinitIframe()", 200);


function chang(cname) {
    window.top.frames['ssi_head'].changNav(cname);
}

/* 表单按钮 */
$(document).ready(function () {
    $(".findBtn").mousedown(function () {
        $(this).css("background", "url(/images/findBtn03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".findBtn").mouseup(function () {
        $(this).css("background", "url(/images/findBtn02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".exportBtn").mousedown(function () {
        $(this).css("background", "url(/images/exportBtn03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".exportBtn").mouseup(function () {
        $(this).css("background", "url(/images/exportBtn02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".importBtn").mousedown(function () {
        $(this).css("background", "url(/images/importBtn03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".importBtn").mouseup(function () {
        $(this).css("background", "url(/images/importBtn02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".deleteBtn").mousedown(function () {
        $(this).css("background", "url(/images/deleteBtn03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".deleteBtn").mouseup(function () {
        $(this).css("background", "url(/images/deleteBtn02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".deleteAllBtn").mousedown(function () {
        $(this).css("background", "url(/images/deleteAllBtn03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".deleteAllBtn").mouseup(function () {
        $(this).css("background", "url(/images/deleteAllBtn02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".addBtn").mousedown(function () {
        $(this).css("background", "url(/images/addBtn03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".addBtn").mouseup(function () {
        $(this).css("background", "url(/images/addBtn02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".newBtn").mousedown(function () {
        $(this).css("background", "url(/images/newBtn03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".newBtn").mouseup(function () {
        $(this).css("background", "url(/images/newBtn02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".uploadBtn").mousedown(function () {
        $(this).css("background", "url(/images/uploadBtn03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".uploadBtn").mouseup(function () {
        $(this).css("background", "url(/images/uploadBtn02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".tab_add").mousedown(function () {
        $(this).css("background", "url(/images/tab_add03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".tab_add").mouseup(function () {
        $(this).css("background", "url(/images/tab_add02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".tab_change").mousedown(function () {
        $(this).css("background", "url(/images/tab_change03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".tab_change").mouseup(function () {
        $(this).css("background", "url(/images/tab_change02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".tab_delete").mousedown(function () {
        $(this).css("background", "url(/images/tab_delete03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".tab_delete").mouseup(function () {
        $(this).css("background", "url(/images/tab_delete02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".leftBtn").mousedown(function () {
        $(this).css("background", "url(/images/leftBtn03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".leftBtn").mouseup(function () {
        $(this).css("background", "url(/images/leftBtn02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".leftAllBtn").mousedown(function () {
        $(this).css("background", "url(/images/leftAllBtn03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".leftAllBtn").mouseup(function () {
        $(this).css("background", "url(/images/leftAllBtn02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".rightAllBtn").mousedown(function () {
        $(this).css("background", "url(/images/rightAllBtn03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".rightAllBtn").mouseup(function () {
        $(this).css("background", "url(/images/rightAllBtn02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".rightBtn").mousedown(function () {
        $(this).css("background", "url(/images/rightBtn03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".rightBtn").mouseup(function () {
        $(this).css("background", "url(/images/rightBtn02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });


    /* tab  */
    $(".tab_dcAll").css("background", "url(/images/tab_dc02.png) no-repeat");
    $(".tab_dcAll").css("color", "#000");

    $(".tab_dcAll").hover(function () {
        $(".tab_dcAll").css("background", "url(/images/tab_dc01.png) no-repeat");
        $(".tab_cxdc").css("background", "url(/images/tab_cz01.png) no-repeat");
        $(this).css("background", "url(/images/tab_dc02.png) no-repeat");
        $(this).css("color", "#000");

    });

    $(".tab_cxdc").hover(function () {
        $(".tab_cxdc").css("background", "url(/images/tab_cz01.png) no-repeat");
        $(".tab_dcAll").css("background", "url(/images/tab_dc01.png) no-repeat");
        $(this).css("background", "url(/images/tab_cz02.png) no-repeat");
        $(this).css("color", "#000");

    });


    /*button*/
    $(".btn_browse").mousedown(function () {
        $(this).css("background", "url(/images/btn_browse03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".btn_browse").mouseup(function () {
        $(this).css("background", "url(/images/btn_browse02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });
    $(".btn_reading").mousedown(function () {
        $(this).css("background", "url(/images/btn_reading03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".btn_reading").mouseup(function () {
        $(this).css("background", "url(/images/btn_reading02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });
    $(".btn_test").mousedown(function () {
        $(this).css("background", "url(/images/btn_test03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".btn_test").mouseup(function () {
        $(this).css("background", "url(/images/btn_test02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });
    $(".btn_collect").mousedown(function () {
        $(this).css("background", "url(/images/btn_collect03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".btn_collect").mouseup(function () {
        $(this).css("background", "url(/images/btn_collect02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });
    $(".btn_close").mousedown(function () {
        $(this).css("background", "url(/images/btn_close03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".btn_close").mouseup(function () {
        $(this).css("background", "url(/images/btn_close02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });
    $(".btn_againImport").mousedown(function () {
        $(this).css("background", "url(/images/btn_againImport03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".btn_againImport").mouseup(function () {
        $(this).css("background", "url(/images/btn_againImport02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });
    $(".btn_definition").mousedown(function () {
        $(this).css("background", "url(/images/btn_definition03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".btn_definition").mouseup(function () {
        $(this).css("background", "url(/images/btn_definition02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".btn_monitoring").mousedown(function () {
        $(this).css("background", "url(/images/btn_monitoring03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".btn_monitoring").mouseup(function () {
        $(this).css("background", "url(/images/btn_monitoring02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".btn_interrupt").mousedown(function () {
        $(this).css("background", "url(/images/btn_interrupt03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".btn_interrupt").mouseup(function () {
        $(this).css("background", "url(/images/btn_interrupt02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });
    $(".btn_alert").mousedown(function () {
        $(this).css("background", "url(/images/btn_alert03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".btn_alert").mouseup(function () {
        $(this).css("background", "url(/images/btn_alert02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });
    $(".btn_reduction").mousedown(function () {
        $(this).css("background", "url(/images/btn_reduction03.png) no-repeat");
        $(this).css("color", "#fff");
    });
    $(".btn_reduction").mouseup(function () {
        $(this).css("background", "url(/images/btn_reduction02.png) no-repeat");
        $(this).css("color", "#037fd4");
    });

    $(".dataTitle_btn").toggle(function () {

        $(this).css("background", "url(/images/SS_iM_siader_tb_up.png) no-repeat");
        $(this).parent().parent().parent().parent().next(".show").hide();
        $($(this).parent().parent()).find(".dtdiv").css("visibility", "hidden");
    }, function () {
        $(this).css("background", "url(/images/SS_iM_siader_tb_down.png) no-repeat");
        $(this).parent().parent().parent().parent().next(".show").show();
        $($(this).parent().parent()).find(".dtdiv").css("visibility", "visible");
    });

    $(".contTitle_right .togbar").toggle(function () {
        $(this).prevAll("input").hide();
        $(this).parent().parent().next("div").hide();
        $(this).parent().find(".ctr_cont").hide();
        $(this).parent().find(".ctr_right").hide();
    }, function () {
        $(this).prevAll("input").show();
        $(this).parent().parent().next("div").show();
        $(this).parent().find(".ctr_cont").show();
        $(this).parent().find(".ctr_right").show();
    });
});


function reload(){
    window.location.reload(true);
}

//get Employee ID by card snr
function getEmpInfoByID(snr) {
    if (!snr || 0 == $.trim(snr).length) {
        return null;
    }
    return DormManageAjaxServices.GetEmployeeInfo(snr);
}
function bindEmpIDCtrl(snrLimit, empID, idcardID, nameID) {
    var elemEmp = $(empID);
    var LIM = snrLimit;
    elemEmp.attr('MaxLength', LIM);

    elemEmp.bind('keypress', function(event) {
        var sold = elemEmp.val();
        var cur = sold.length;
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if ((13 == keycode) /*|| (cur && cur>= LIM-1)*/) {
            event.preventDefault();

            setTimeout(function () {
                var sret = getEmpInfoByID(elemEmp.val());
                var sIdCard = sName = "";
                if (sret && sret.value) {
                    var empInfo = sret.value;                    
                    elemEmp.val(empInfo.empID);
                    sIdCard = empInfo.idCardNum;
                    sName = empInfo.sname;
                }
                if (idcardID && idcardID !== "") {
                    var elemIdcard = $(idcardID);
                    elemIdcard.val(sIdCard);
                }
                if (nameID && nameID !== "") {
                    var elemIdname = $(nameID);
                    elemIdname.val(sName);
                }
            }, 900);
        }
    });
}

function isIE() {
    var s = navigator.userAgent;        
    return s.indexOf('MSIE')>-1 || s.indexOf('Trident')>-1;
}

function CanChangeLeaveSign(sel) {
    var sarr = ["自离", "外住", "未入职", "调房"];
    return $.inArray(sel, sarr) < 0;
    //return sarr.indexOf(sel) < 0;
}
function RefreshReason(sel, chkID) {
    var bEnable = CanChangeLeaveSign(sel);
    $(chkID).prop("disabled", !bEnable);
    $(chkID).attr("checked", bEnable); //默认选中
    //if (!bEnable) {
    //    $(chkID).attr("checked", false);
    //}
}
function BindReasonChange(selID, chkID){
    $(selID).change(function () {
        var sel = this.value;
        RefreshReason(sel, chkID);
    });
}

/**
 * @author johnsing
 * @param idcard : 中国身份证号码
 * @returns true for success
 */
function CheckIdCard(idcard) {
    var area = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外" };
    var Y, JYM;
    var S, M;
    var idcard_array = idcard.split("");
    //1.地区检验
    if (area[parseInt(idcard.substr(0, 2))] == null) {
        return false;
    }
    //2.身份号码位数及格式检验 
    switch (idcard.length) {
        case 18:
            //18位身份号码检测 
            //出生日期的合法性检查
            if (parseInt(idcard.substr(6, 4)) % 4 == 0 || (parseInt(idcard.substr(6, 4)) % 100 == 0 && parseInt(idcard.substr(6, 4)) % 4 == 0)) {
                ereg = /^[1-9][0-9]{5}(19|20)[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9Xx]$/;//闰年出生日期的合法性正则表达式 
            } else {
                ereg = /^[1-9][0-9]{5}(19|20)[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9Xx]$/;//平年出生日期的合法性正则表达式 
            }
            if (ereg.test(idcard)) {
                S = (parseInt(idcard_array[0]) + parseInt(idcard_array[10])) * 7
                    + (parseInt(idcard_array[1]) + parseInt(idcard_array[11])) * 9
                    + (parseInt(idcard_array[2]) + parseInt(idcard_array[12])) * 10
                    + (parseInt(idcard_array[3]) + parseInt(idcard_array[13])) * 5
                    + (parseInt(idcard_array[4]) + parseInt(idcard_array[14])) * 8
                    + (parseInt(idcard_array[5]) + parseInt(idcard_array[15])) * 4
                    + (parseInt(idcard_array[6]) + parseInt(idcard_array[16])) * 2
                    + parseInt(idcard_array[7]) * 1
                    + parseInt(idcard_array[8]) * 6
                    + parseInt(idcard_array[9]) * 3;
                Y = S % 11;
                M = "F";
                JYM = "10X98765432";
                M = JYM.substr(Y, 1);
                if (M == idcard_array[17]) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
            break;
        default:
            return false;
            break;
    }
}
