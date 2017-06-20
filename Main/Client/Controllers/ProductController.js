app.controller('ProductController', ['$scope', '$http', '$routeParams', ($scope, $http, $routeParams) => {
    console.log($routeParams.id);
    return;
    $http(
            {
                method: 'GET',
                url: '/api/products/' + $routeParams.id,
                data: $httpParamSerializerJQLike({ order: JSON.stringify(cartService.getProducts()), clientinfo: JSON.stringify($scope.userInfo) }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }
        ).then(
            (response) => {
                if (!response.data.status) {
                    errorService.setError('responseError', response.data);
                    $location.path('/failure');
                }
                else
                    $location.path('/succeed');

                console.log(response.data);
            },
            (error) => {
                console.log(error);
            }
        );
}]);