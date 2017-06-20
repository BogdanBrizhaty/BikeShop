var routeConfig = app.config(['$routeProvider', ($routeProvider) => {
    $routeProvider.when('/', {
        templateUrl: 'Client/Views/Home.html',
        Controller: 'HomeController'
    });
    $routeProvider.when('/products/page/:pageNumber', {
        templateUrl: 'Views/Home.html',
        Controller: 'ProductController'
    });
    $routeProvider.when('/products/', {
        templateUrl: '/products/page/1'
    });
    $routeProvider.when('/products/:id', {
        templateUrl: 'Views/ProductView.html',
        Controller: 'ProductController'
    });
    $routeProvider.when('/search/:q', {
        templateUrl: 'Views/Home.html',
        Controller: 'SearchController'
    });
}])