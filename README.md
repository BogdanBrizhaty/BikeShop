# BikeStore

Simple test application as part of AngularJS learning.
BackEnd made using ASP.Net Web Api. Used nInject library to resolve DI into the ProductController. 
All data is operating through IProductService and IDataService(Which mostly defines just a repository ) interface.
In case of needs to send products metadata(such as count of items/pages/etc) to client, all of theese is sent through HTTP response headers.
First I tried to send the metdata through the body of response in format of object that contains both 
data and meta ({ totalItems: ..., data:List<Product>} f.eg.). But if some abstract app would need to retrieve these metadata more times,
the answer from controller may be too big and get too much time to recieve it. So I found the way of doing HEAD-requests to controllers, 
so it would return all needed metainfo in header and no additional needs to call the body part.
