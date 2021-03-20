///** add active class and stay opened when selected */
//let url = window.location
//let protocol = window.location.protocol;
//let host = window.location.host;

let pathname = window.location.pathname;
let pathNameArray = pathname.split("/");
let currentController = pathNameArray[1].toLowerCase();
//alert(currentController);

// for sidebar menu entirely but not cover treeview
$('ul.nav-sidebar a').filter(function () {
    let hrefController = this.href.split("/");
    let selectedController = ("" + hrefController[3]).toLowerCase();
    return selectedController != currentController;
}).removeClass('active');

// for treeview
$('ul.nav-sidebar a').filter(function () {
    let hrefController = this.href.split("/");
    let selectedController = ("" + hrefController[3]).toLowerCase();
    return selectedController == currentController;
}).parentsUntil(".nav-sidebar > .nav-link").addClass('active');

// for sidebar menu entirely but not cover treeview
$('ul.nav-sidebar a').filter(function () {
    let hrefController = this.href.split("/");
    let selectedController = ("" + hrefController[3]).toLowerCase();
    return selectedController == currentController;
}).addClass('active');