///** add active class and stay opened when selected */
//let url = window.location
//let protocol = window.location.protocol;
//let host = window.location.host;

let href = window.location.origin + window.location.pathname;
//alert(currentController);

// for sidebar menu entirely but not cover treeview
$('ul.nav-sidebar a').filter(function () {
    return href != this.href;
}).removeClass('active');

// for treeview
$('ul.nav-sidebar a').filter(function () {
    return href == this.href;
}).parentsUntil(".nav-sidebar > .nav-link").addClass('active');

// for sidebar menu entirely but not cover treeview
$('ul.nav-sidebar a').filter(function () {
    return href == this.href;
}).addClass('active');