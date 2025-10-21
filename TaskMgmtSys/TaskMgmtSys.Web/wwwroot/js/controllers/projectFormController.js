app.controller('projectFormController', function ($scope, $http, $window) {
    $scope.project = {
        id: 0,
        projectName: "",
        description: ""
    };
    $scope.successMessage = "";
    $scope.errorMessage = "";

    $scope.loadProject = function (id) {
        $http.get('/api/projects/' + id)
            .then(function (response) {
                $scope.project = response.data;
            })
            .catch(function (err) {
                $scope.errorMessage = "Error loading project.";
            });
    };

    $scope.saveProject = function () {
        const url = ($scope.project.Id && $scope.project.Id > 0) ? '/Projects/Edit/' + $scope.project.Id : '/Projects/Create';

        const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
        const token = tokenInput ? tokenInput.value : null;

        const config = token ? { headers: { 'RequestVerificationToken': token } } : {};

        console.log("$scope.project", $scope.project);

        $http.post(url, $scope.project, config)
            .then(function (response) {
                $scope.successMessage = $scope.project.Id
                    ? "Project updated successfully!"
                    : "Project created successfully!";
                setTimeout(() => $window.location.href = '/Projects', 1500);
            })
            .catch(function (error) {
                console.log(error);
                $scope.errorMessage = "Error saving project.";
            });
    };
});