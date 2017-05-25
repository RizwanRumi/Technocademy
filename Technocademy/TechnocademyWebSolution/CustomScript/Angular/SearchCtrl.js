var app = angular.module("MyApp", []);
app.controller('SearchController', ['$scope', '$http', function ($scope, $http) {
    $scope.VideoList = [];
    $scope.setCategory = "General";
    
    $scope.tab = 1;
    $scope.selectTab = function (setTab) {
        $scope.tab = setTab;
    };
    $scope.isSelected = function (checkTab) {
        return $scope.tab === checkTab;
    };

    $scope.initialCourse = 0;

    
    var count = -10;
    var searchValue = "";

    var scontroller = new ScrollMagic.Controller();

    var scene = new ScrollMagic.Scene({ triggerElement: ".dynamicContent #loader", triggerHook: "onEnter" })
    .addTo(scontroller)
    .on("enter", function () {
        if (!$("#loader").hasClass("active")) {
            $("#loader").addClass("active");
            setTimeout(addVideos, 1000, count += 10, searchValue);
        }
    });

    function addVideos(amount, searchValue) {
           
        var courseValue = $scope.initialCourse;
           
        $scope.code = null;
        $scope.response = null;

        $http({ method: 'GET', url: "/Search/SearchVideo?id=" + courseValue + "&val=" + amount + "&search=" + searchValue })
            .then(function(response) {
                        
                for (var i = 0; i < response.data.length; i++) {
                    $scope.VideoList.push(response.data[i]);
                }
                scene.update();
                $("#loader").removeClass("active");
            }, function(response) {
                alert(response);
            });                      
    };

    $scope.DateSplit = function(input) {
        var res = input.split("(");
        return res[1].split(")")[0];
    };
       
   
    $scope.courseList = [];  

    $http.get('/Search/GetAllCourses').then(function (response) {
        for (var i = 0; i < response.data.length; i++) {
            $scope.courseList.push(response.data[i]);
        }
       // $scope.courseList = response.data;
    }, function (response) {
        alert(response);
    });

    $http.get('/Search/GetAllCategories').success(function (result) {
            $scope.categoryList = result;
        }).error(function(result) {
            alert(result);
        });

        $scope.rating = function(obj, tblId) {

            var htmlReatingTable = '<tr>';
            for (var i = 0; i < obj; i++) {
                htmlReatingTable += '<td><img class="starIcon" src="/Content/icon/rating.png"></img></td>';
            }
            htmlReatingTable += '</tr>';

            $('.ttl_' + tblId).find('table').html(htmlReatingTable);
        }

        $scope.ListRating = function (obj, tblId) {

            var htmlReatingTable = '<td>Ratings: </td>';
            for (var i = 0; i < obj; i++) {
                htmlReatingTable += '<td><img class="starIcon" src="/Content/icon/rating.png"></img></td>';
            }
            htmlReatingTable += '';

            $('.list_' + tblId).find('table').html(htmlReatingTable);
        }

        $scope.sortedByCourse = function (courseVal, courseNam) {
                $scope.VideoList = [];
                $scope.setCategory = courseNam;
                $scope.initialCourse = courseVal;
                count = 0;
               // alert($scope.initialCourse + ", " + $scope.setCategory + ", " + count);
                addVideos(count, '');
        };


        $scope.SearchResult = function (searchVal) {
           
            $scope.VideoList = [];
            searchValue = searchVal;
            $scope.initialCourse = 0;
            count = 0;
            addVideos(count, searchValue);
        };
   
}]);
