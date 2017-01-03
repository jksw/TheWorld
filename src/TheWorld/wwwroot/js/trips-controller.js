// tripsController.js
// immediately invoking function expression, purpose is to keep out of global scope
(function () {
  "use strict";
  //Specify a module where the code lives

  //Return an object on which we can add a controller
  //Getting the existing module -- that's what no [] means
  angular.module("app-trips")
  .controller("tripsController", tripsController);

  //For the example with just a local object returned to ui, this works:
  //function tripsController() {

  // but to get data from server, inject $http service
  //Think of the $ as indicative that Angular provided it
  function tripsController($http) {
    //SW just likes to do it this way.
    // 'this' means the object that is returned from the tirps controller.   liek a clas, but prototypeical.

    //SW likes vm (for view model) cuz it's more descriptive
    var vm = this;
    //////////////vm.trips = [{
    //////////////  name: "US Trips",
    //////////////  created: new Date()
    //////////////},
    //////////////{
    //////////////  name: "World Trip",
    //////////////  created: new Date()
    //////////////}
    //////////////];

    //now get real data from server
    vm.trips = [];

    vm.errorMessage = "";
    vm.isBusy = true;

    $http.get("/api/trips")
      // Success 
      .then(function (response) {

        //Already been converted from Json into object
        angular.copy(response.data, vm.trips);
        // fail        
      }, function (error) {
        vm.errorMessage = "Failed to load data: " + error;
      })
    .finally(function () {
      vm.isBusy = false;
    });

    vm.addTrip = function () {
      vm.isBusy = true;
      vm.errorMessage = "";
      $http.post("/api/trips", vm.newTrip)
       // Success 
        .then(function (response) {

          //Already been converted from Json into object
          // dont' get confused here: the post is going to post the vm.newTrip; the post method
          // is writtent to return what was posted, so that ... we can then pouplate it into the form
          vm.trips.push(response.data);
          vm.newTrip = {};
          // fail        
        }, function () {
          vm.errorMessage = "Failed to save new trip" + error;
        })
      .finally(function () {
        vm.isBusy = false;
      });
      //////////////////vm.addTrip = function () {
      //////////////////  vm.trips.push({ name: vm.newTrip.name, created: new Date() });
      //////////////////  //tell the form that the data is gone; clear the form.  
      //////////////////  vm.newTrip = {};
    }
  }
}
)();