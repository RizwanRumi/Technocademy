var information = {
    Name: "Video Upload",
    Labels: [
          "Beginner",
          "Intermediate",
          "Advance"
        ]
};


app.controller('videoController', ['$scope', '$http', function ($scope, $http) {
    $scope.TitleName = information.Name;
    $scope.LabelList = information.Labels;
    $scope.VideoList = [];
    $scope.date = new Date();


    $scope.states = {
        showVideoForm: false,
        errorVideo: false
        //isAdding: false  // for spin
    };

    $scope.new = {
        Video: {}
    };

    $scope.showVideoForm = function (show) {
        $scope.states.showVideoForm = show;
        $scope.states.errorVideo = false;
    };


    $http.get('/VideoUploads/GetAllVideo')
        .success(function (result) {
            $scope.VideoList = result;
            //$scope.information.LabelList = information.Labels;
        }).error(function(result) {
            alert(result);
        });

    $scope.DateSplit = function (input) {
        var res = input.split("("); 
        return res[1].split(")")[0];
    };

    $http.get('/VideoUploads/GetAllCategories')
        .success(function (caData) {
            $scope.categoryList = caData;
        })
        .error(function (e) {
            alert(e);
        });


    $scope.addVideo = function () {
      
        if ($scope.new.Video.VideoName != null && $scope.new.Video.CategoryId != null && $scope.new.Video.Label != null && $scope.new.Video.Ratings != null && $scope.new.Video.Lecturer != null) {

            var files = $("#UploadvdoFile")[0].files;

            var data = new FormData();
            data.append('vdoFile', files[0]);
            data.append('vdoName', $scope.new.Video.VideoName);
            data.append('vdoCaId', $scope.new.Video.CategoryId);
            data.append('vdoLbl', $scope.new.Video.Label);
            data.append('vdoRating', $scope.new.Video.Ratings);
            data.append('vdoLecturer', $scope.new.Video.Lecturer);

            $http.post('/VideoUploads/Create', data, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
           .success(function (result) {
               $scope.VideoList.push(result);
               $scope.states.showVideoForm = false;
               $scope.new.Video = {};
               $scope.states.errorVideo = false;
           })
           .error(function (e) {
               alert("file size is too large.");
           });
        } else {
            $scope.states.errorVideo = true;
        }


    };



    $scope.EditVideo = function (item) {
        
        var files = $("#evdoFileUpload")[0].files;
        
        var data = new FormData();
        data.append('evdoFile', files[0]);
        data.append('evdoFilePath', item.FilePath);
        data.append('evdoUpId', item.VideoUploadId);
        data.append('evdoName', item.VideoName);
        data.append('evdoCaId', item.CategoryId);
        data.append('evdoLbl', item.Label);
        data.append('evdoRating', item.Ratings);
        data.append('evdoLecturer',item.Lecturer);

        $http.post('/VideoUploads/Edit', data, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })
       .success(function (result) {
           if (result == 'error') {
               alert("please file upload");
           }
       })
       .error(function (e) {
                alert(e);
            });

        //$.ajax({
        //    type: "POST",
        //    url: "Edit",
        //    data: data,
        //    processData: false,
        //    contentType: false,
        //    success: function (result) {

        //    },
        //    error: function (result) { }
        //});

      
    };
    
    $scope.DeleteVideo = function(index,vdoId) {
        $http.get('/VideoUploads/DeleteConfirmed/' + vdoId).success(function () {
            $scope.VideoList.splice(index, 1);
        }).error(function(result) {
            alert(result);
        });
    };
}]);