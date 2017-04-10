
//====页面动态加载C-Lodop云打印必须的文件CLodopfuncs.js====
/*
var head = document.head || document.getElementsByTagName("head")[0] || document.documentElement;

//让其它电脑的浏览器通过本机打印（仅适用C-Lodop自带的例子）：
var oscript = document.createElement("script");
oscript.src = "/CLodopfuncs.js";
head.insertBefore(oscript, head.firstChild);

//让本机的浏览器打印(更优先一点)：
oscript = document.createElement("script");
oscript.src = "http://localhost:8000/CLodopfuncs.js?priority=2";
head.insertBefore(oscript, head.firstChild);

//加载双端口(8000和18000）避免其中某个端口被占用：
oscript = document.createElement("script");
oscript.src = "http://localhost:18000/CLodopfuncs.js?priority=1";
head.insertBefore(oscript, head.firstChild);
*/


function needCLodop() {
    try {
        var ua = navigator.userAgent;
        if (ua.match(/Windows\sPhone/i) != null) return true;
        if (ua.match(/iPhone|iPod/i) != null) return true;
        if (ua.match(/Android/i) != null) return true;
        if (ua.match(/Edge\D?\d+/i) != null) return true;

        var verTrident = ua.match(/Trident\D?\d+/i);
        var verIE = ua.match(/MSIE\D?\d+/i);
        var verOPR = ua.match(/OPR\D?\d+/i);
        var verFF = ua.match(/Firefox\D?\d+/i);
        var x64 = ua.match(/x64/i);
        if ((verTrident == null) && (verIE == null) && (x64 !== null))
            return true;
        else if (verFF !== null) {
            verFF = verFF[0].match(/\d+/);
            if ((verFF[0] >= 42) || (x64 !== null)) return true;
        } else if (verOPR !== null) {
            verOPR = verOPR[0].match(/\d+/);
            if (verOPR[0] >= 32) return true;
        } else {
            if ((verTrident == null) && (verIE == null)) {
                var verChrome = ua.match(/Chrome\D?\d+/i);
                if (verChrome !== null) {
                    verChrome = verChrome[0].match(/\d+/);
                    if (verChrome[0] >= 42) return true;
                };
            };
        }
        return false;
    } catch (err) { return true; };
};

//====获取LODOP对象的主过程：====
function getLodop(oOBJECT, oEMBED) {
    var LODOP;
    try {
        try {
            LODOP = getCLodop();
        } catch (e) {
            alert("[1]getLodop出错:" + e);
        }
        if (!LODOP && document.readyState !== "complete") {
            alert("C-Lodop没准备好，请稍后再试！");
            return null;
        };
        //清理原例子内的object或embed元素，避免乱提示：
        if (oEMBED && oEMBED.parentNode) oEMBED.parentNode.removeChild(oEMBED);
        if (oOBJECT && oOBJECT.parentNode) oOBJECT.parentNode.removeChild(oOBJECT);
    } catch (err) {
        alert("[2]getLodop出错:" + err);
    };

    //===如下空白位置适合调用统一功能(如注册语句、语言选择等):===
    LODOP.SET_SHOW_MODE("NP_NO_RESULT", true);//设置NP插件无返回，这可避免chrome对弹窗超时误报崩溃
    //LODOP.SET_LICENSES("", "13528A153BAEE3A0254B9507DCDE2839", "", "");
    //===========================================================

    return LODOP;
};