//  site.js 

// Avoid global Scope 
(
  function () {

    //From Practical jQuery module

    var $sidebarAndWrapper = $("#sidebar,#wrapper");

    //Find italic children of sidebar toggle classed as fa

    var $icon = $("#sidebarToggle i.fa");

    $("#sidebarToggle").on("click", function () {
      $sidebarAndWrapper.toggleClass("hide-sidebar");
      if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
        $icon.removeClass("fa-angle-left");
        $icon.addClass("fa-angle-right");
      } else {
        $icon.addClass("fa-angle-left");
        $icon.removeClass("fa-angle-right");
      }
    });


    // -------- commented code below was Shawn's demo of jQuery functionality 

    ////var ele = document.getElementById("username");
    ////ele.innerHTML = "steve";

    ////Jquery version of above
    //var ele = $("#username");
    //ele.text("steve");

    ////var main = document.getElementById("main");
    ////main.onmouseenter = function () {
    ////  main.style.backgroundColor = "#888";
    ////};

    ////main.onmouseleave = function () {
    ////  main.style.backgroundColor = "";
    ////};

    ////jQuery version of above
    //var main = $("#main");
    //main.on("mouseenter", function () {
    //  main.css('background-color', '#888');
    //});

    //main.on("mouseleave", function () {
    //  main.css('background-color', '');
    //});

    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function () {

    //  //this = object that the function is related to
    //  //$(this) means "make this into a jQuery object for me" in this case, the text within the anchor tag
    //  var me = $(this);
    //  alert(me.text());
    //});

  })();

