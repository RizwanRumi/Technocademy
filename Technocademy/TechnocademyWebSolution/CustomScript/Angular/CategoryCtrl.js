
var information = {
    Name: 'Tutorial Category',
    categoryitems: []
};

app.controller('categoryController', ['$scope', '$http', function ($scope, $http) {
    $scope.categoryName = information.Name;

    $scope.states = {
        showCategoryForm: false,
        errorCategory: false,
        isAdding: false  // for spin
    };

    $scope.new = {
        Category: {}
    };

    
    $http.get('/Category/GetAllCategories')
        .success(function (data) {
            $scope.categoryList = data;
            
        })
        .error(function(data) {
            alert(data);
        });
    $scope.selectedOption = { CourseId: '4' };

    $http.get('/Course/GetAllCourses')
        .success(function (courseData) {
            $scope.courseList = courseData;
        })
        .error(function (courseData) {
            alert(courseData);
    });
    
    $scope.showCategoryForm = function (show) {
        $scope.states.showCategoryForm = show;
        $scope.states.errorCategory = false;
    };

    $scope.errorCategory = false;

    $scope.addCategory = function () {
        //$scope.states.isAdding = true;
        //alert($scope.new.Category.CourseId);
        if ($scope.new.Category.CategoryName != null && $scope.new.Category.CourseId != null) {
            $http.post('/Category/Create', $scope.new.Category)
            .success(function (data) {
                if (data != 'no') {
                    $scope.states.isAdding = false;
                    $scope.categoryList.push(data);
                    $scope.states.showCategoryForm = false;
                    $scope.new.Category = {};
                    $scope.errorCategory = false;
                } else {
                    alert("category name already exist!");
                }
            })
            .error(function (data) {
                alert(data);
            });
        } else {
            $scope.states.errorCategory = true;
        }

    };

    $scope.EditCategory = function(item) {
        if (item != null) {
           $http.post('/Category/Edit', item)
                .success(function (data) { })
                .error(function (data) {
                    alert(data);
                });
        } else {
            alert("Reload  page or select on option in your browser javascript");
        }
    };



    $scope.DeleteCategory = function (index, id) {
        $http.get('/Category/DeleteConfirmed/' + id)
            .success(function (result) {
                $scope.categoryList.splice(index, 1);
            })
            .error(function (result) {
                alert(result);
            });

    }

}]);


