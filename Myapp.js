var app = angular.module("MyApp", ['ngRoute']);

app.config(function ($routeProvider, $locationProvider) {
    console.log($routeProvider);
    $routeProvider
        .when('/home', {
            templateUrl: "/Template/home.html",
            controller: 'EmployeeController'
        })
        .when('/addemployee', {
            templateUrl: "/Template/AddEmployee.html",
            controller: 'AddController'
        })
        .when('/contact', {
            templateUrl: "/Template/ViewEmployees.html",
            controller: 'MainController'
        })

    .otherwise({
        templateUrl: "/Template/home.html",
        controller: 'EmployeeController'
    })


});
    

//Controller to Get, Add Update and Delete from API
app.controller("EmployeeController", function ($scope, $http) {
    $http.get('http://localhost:21614/api/getEmployee')
        .then(function (response) {
            $scope.users = response.data;
        })
        .catch(function () {
            alert("There was a server side error")
        });
    //Get a record from API by id
   
});
app.controller("AddController", function ($scope, $http) {
   
     $scope.tab = 1;
        $scope.isSelected = function (tab) {
            return $scope.tab === tab;
        }
        $scope.selTab = function (tab) {
            $scope.tab = tab;
        }
    });

app.controller("MainController", function ($scope, $http) {
});

app.directive("addEmp", function () {
    return {
        restrict: 'ACE',
        templateUrl: "/Home/AddEmployee"
    }
});
app.directive("qualificationEmp", function () {
    return {
        restrict: 'ACE',
        templateUrl: "/Home/Qualification"
    }
});
app.directive("communicationEmp", function () {
    return {
        restrict: 'ACE',
        templateUrl: "/Home/Communication"
    }
});
//Directive to display the data in grid
app.directive("gridView", function () {
    return {
        restrict: 'ACE',
        templateUrl: "/Home/EmployeeTable"
    }
});

//Directive to get the popup
app.directive("popupView", function () {
    return {
        restrict: 'ACE',
        templateUrl: "/Home/PopupView"
    }
});
function myFunction() {
    var input, filter, table, tr, td, i;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}