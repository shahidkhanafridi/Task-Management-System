// Define AngularJS module
var app = angular.module("taskApp", []);

// Global layout controller (used in _Layout)
app.controller("layoutController", function ($scope) {
    $scope.appName = "Task Management System";
});