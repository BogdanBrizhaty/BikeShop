var routeConfig = app.config(['$routeProvider', ($routeProvider) => {
    $routeProvider.when('/', {
        templateUrl: 'Client/Views/Home.html',
        Controller: 'HomeController'
    });
    //$routeProvider.when('/products', {
    //    redirectTo: '/products/page/1'
    //});
    //$routeProvider.when('/products/page', {
    //    redirectTo: '/products/page/1'
    //});
    $routeProvider.when('/products/:id', {
        templateUrl: 'Client/Views/Product.html',
        Controller: 'ProductController' 
    });
    $routeProvider.when('/products', {
        //$routeProvider.when('/products/page/:pageNumber', {
        templateUrl: 'Client/Views/Products.html',
        Controller: 'ProductsController'
    });
    $routeProvider.when('/search/:q', {
        templateUrl: 'Client/Views/Search.html',
        Controller: 'SearchController'
    });
}])