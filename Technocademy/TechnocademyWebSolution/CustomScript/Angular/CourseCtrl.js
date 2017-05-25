
var information = {
    Name: 'Tutorial Course',
    items: []
};

    
app.controller('courseController',['$scope','$http',function ($scope, $http) {
    $scope.cName = information.Name;

    $scope.states = {
        showCourseForm: false,
        errorCourse: false,
        isAdding: false
    };
    $scope.new = {
        Course: {}
    };
        
    $http.get('/Course/GetAllCourses').success(function(data) {
        $scope.courseList = data;
    });
    //$scope.courseList = information.items;

    $scope.showCourseForm = function (show) {
        $scope.states.showCourseForm = show;
        $scope.states.errorCourse = false;
    };

    $scope.errorCourse = false;

    $scope.addCourse = function () {
        if ($scope.new.Course.CourseName != null) {
            $http.post('/Course/Create', $scope.new.Course)
            .success(function (data) {
                    if (data != 'no') {
                        $scope.states.isAdding = false;
                        $scope.courseList.push(data);
                        $scope.states.showCourseForm = false;
                        $scope.new.Course = {};
                        $scope.errorCourse = false;
                    } else {
                        alert("course name already exist!");
                    }
                })
            .error(function (data) {
                alert(data);
            });
        } else {
            $scope.states.errorCourse = true;
        }
            
    };

    $scope.EditCourse = function (item) {
        //$scope.states.isAdding = true;
        //$scope.new.Course.CourseId = id;
        // alert(item.CourseName);
        //item.CourseName = newName;
        if (item != null) {
            $http.post('/Course/Edit',item)
            .success(function (data) {
           
            })
            .error(function (data) {
                alert(data);
            });
        } else {
            alert("Reload  page or select on option in your browser javascript");
        }

    };


    $scope.DeleteCourse = function (index,id) {
        $http.get('/Course/DeleteConfirmed/' + id)
            .success(function (result) {
                $scope.courseList.splice(index, 1);
            })
            .error(function(result) {
                alert(result);
            });

    }

}]);
    
