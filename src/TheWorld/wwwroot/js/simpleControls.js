// simpleControls.js
// Add controls can reuse.

// Immediately invoked function expression to avoid using global scope
(function () {
  "use strict";

  //create a new angular module -- signified by the []
  //  then create a new resuable component that shows a wait cursor
  //    would be invoked like this:   <wait-cursor ng-show="vm.isBusy"></wait-cursor>
  angular.module("simpleControls", [])
  .directive("waitCursor", waitCursor);

  //note that /view refers to folder inside wwwroot -- this is client side
  //But also note that Trips.cshtml needs reference to this simpleControls.js file
  function waitCursor() {
    return {
      templateUrl: "/views/waitCursor.html"
    };
  }
})();

