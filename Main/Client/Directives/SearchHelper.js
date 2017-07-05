app.directive('searchHelper', ['$location', function ($location) {
    return {
        restrict: 'A',
        scope: true,
        controller: function ($scope, $location) {
            $scope.q = '';
            $scope.submitQuery = () => {
                if ($scope.q === '')
                    return;
                $location.path('/search/' + $scope.q);
                }
        }
    };
}]);