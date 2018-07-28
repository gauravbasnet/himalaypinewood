"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var Rx_1 = require("rxjs/Rx");
require("rxjs/add/operator/map");
require("rxjs/add/operator/catch");
exports.domain = 'http://localhost:1234/';
exports.authorizationData = function () {
    return JSON.parse(localStorage.getItem('authorizationData'));
};
var Service = /** @class */ (function () {
    function Service(http) {
        this.http = http;
        this.baseUrl = "";
    }
    Object.defineProperty(Service, "parameters", {
        get: function () {
            return [[http_1.Http]];
        },
        enumerable: true,
        configurable: true
    });
    Service.prototype.getAll = function (query) {
        if (query != null) {
            query = "?" + query;
        }
        else {
            query = '';
        }
        return this.http.get(this.baseUrl + query).map(function (res) {
            if ((res.json()["odata.count"]) != null) {
                return res.json();
            }
            else {
                return res.json().value;
            }
        }).catch(function (err) {
            return Rx_1.Observable.throw(err);
        });
    };
    Service.prototype.get = function (id, query) {
        if (query != null) {
            query = "?" + query;
        }
        else {
            query = '';
        }
        return this.http.get(this.baseUrl + ("(" + id + ")") + query).map(function (res) {
            return res.json();
        }).catch(function (err) {
            return Rx_1.Observable.throw(err);
        });
    };
    Service.prototype.put = function (id, item) {
        return this.http.put(this.baseUrl + ("(" + id + ")"), item).map(function (res) {
            return res.json();
        }).catch(function (err) {
            return Rx_1.Observable.throw(err);
        });
    };
    Service.prototype.post = function (item) {
        return this.http.post(this.baseUrl, item).map(function (res) {
            return res.json();
        }).catch(function (err) {
            console.log(err);
            return Rx_1.Observable.throw(err);
        });
    };
    Service.prototype.delete = function (id) {
        return this.http.delete(this.baseUrl + ("(" + id + ")")).map(function (res) {
            return res.json();
        }).catch(function (err) {
            return Rx_1.Observable.throw(err);
        });
    };
    return Service;
}());
exports.Service = Service;
var BrandService = /** @class */ (function (_super) {
    __extends(BrandService, _super);
    function BrandService() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.baseUrl = exports.domain + "odata/Brands";
        return _this;
    }
    BrandService = __decorate([
        core_1.Injectable()
    ], BrandService);
    return BrandService;
}(Service));
exports.BrandService = BrandService;
var ClientService = /** @class */ (function (_super) {
    __extends(ClientService, _super);
    function ClientService() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.baseUrl = exports.domain + "odata/Clients";
        return _this;
    }
    ClientService = __decorate([
        core_1.Injectable()
    ], ClientService);
    return ClientService;
}(Service));
exports.ClientService = ClientService;
var ProductMainService = /** @class */ (function (_super) {
    __extends(ProductMainService, _super);
    function ProductMainService() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.baseUrl = exports.domain + "odata/ProductMains";
        return _this;
    }
    ProductMainService = __decorate([
        core_1.Injectable()
    ], ProductMainService);
    return ProductMainService;
}(Service));
exports.ProductMainService = ProductMainService;
var ProductTypeService = /** @class */ (function (_super) {
    __extends(ProductTypeService, _super);
    function ProductTypeService() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.baseUrl = exports.domain + "odata/ProductTypes";
        return _this;
    }
    ProductTypeService = __decorate([
        core_1.Injectable()
    ], ProductTypeService);
    return ProductTypeService;
}(Service));
exports.ProductTypeService = ProductTypeService;
var EnquiryService = /** @class */ (function (_super) {
    __extends(EnquiryService, _super);
    function EnquiryService() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.baseUrl = exports.domain + "odata/Enquirys";
        return _this;
    }
    EnquiryService = __decorate([
        core_1.Injectable()
    ], EnquiryService);
    return EnquiryService;
}(Service));
exports.EnquiryService = EnquiryService;
var UserService = /** @class */ (function (_super) {
    __extends(UserService, _super);
    function UserService() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.baseUrl = exports.domain + "odata/Users";
        return _this;
    }
    UserService = __decorate([
        core_1.Injectable()
    ], UserService);
    return UserService;
}(Service));
exports.UserService = UserService;
//# sourceMappingURL=BaseServices.js.map