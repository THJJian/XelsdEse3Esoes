;
var CSPPLib = (function () {
    if (!window.console) {
        window.console = {};
        window.console.log = function () {
        };
    }
    return {
        RegNameSpace: function (ns) {
            var domains = ns.split(".");
            var cur_domain = CSPPLib;
            for (var i = 0; i < domains.length; i++) {
                var domain = domains[i];
                if (typeof (cur_domain[domain]) === "undefined") {
                    cur_domain[domain] = {};
                }
                cur_domain = cur_domain[domain];
            }
            return cur_domain;
        }
    };
})();