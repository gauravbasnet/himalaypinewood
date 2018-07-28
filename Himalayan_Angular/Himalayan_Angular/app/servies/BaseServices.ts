import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { IBrand, IClients, IEnquiry, IProductMain, IProductType, IUser } from '../model/model';

export const domain: string = 'http://localhost:1234/';


export var authorizationData = () => {
    return JSON.parse(localStorage.getItem('authorizationData'))
};

export interface IService {
    getAll(query?: string): Observable<any>;
    get(id: number, query?: string): Observable<any>;
    put(id: number, item: any): Observable<any>;
    post(item?: any): Observable<any>;
    delete(id: number): Observable<any>;
}


export class Service<T> implements IService {
    public baseUrl = ``;

    static get parameters() {
        return [[Http]];
    }

    constructor(public http: Http) { }

    getAll(query?: string): Observable<T[]> {
        if (query != null) {
            query = `?${query}`;
        } else {
            query = '';
        }

        return this.http.get(this.baseUrl + query).map((res: any) => {
            if (<any>(res.json()[`odata.count`]) != null) {
                return res.json();
            } else {
                return res.json().value;
            }
        }).catch((err) => {
            return Observable.throw(err);
        });
    }

    get(id: any, query?: string): Observable<T> {
        if (query != null) {
            query = `?${query}`;
        } else {
            query = '';
        }

        return this.http.get(this.baseUrl + `(${id})` + query).map((res: Response) => {
            return res.json();
        }).catch((err) => {
            return Observable.throw(err);
        });
    }

    put(id: any, item?: T): Observable<T> {
        return this.http.put(this.baseUrl + `(${id})`, item).map((res: Response) => {
            return res.json();
        }).catch((err) => {
            return Observable.throw(err);
        });
    }

    post(item?: T): Observable<T> {
        return this.http.post(this.baseUrl, item).map((res) => {
            return res.json();
        }).catch((err) => {
            console.log(err);
            return Observable.throw(err);
        });
    }

    delete(id: any): Observable<T> {
        return this.http.delete(this.baseUrl + `(${id})`).map((res: Response) => {
            return res.json();
        }).catch((err) => {
            return Observable.throw(err);
        });
    }
}


@Injectable()
export class BrandService extends Service<IBrand>{
    public baseUrl = `${domain}odata/Brands`;
}
@Injectable()
export class ClientService extends Service<IClients>{
    public baseUrl = `${domain}odata/Clients`;
}
@Injectable()
export class ProductMainService extends Service<IProductMain>{
    public baseUrl = `${domain}odata/ProductMains`;
}
@Injectable()
export class ProductTypeService extends Service<IProductType>{
    public baseUrl = `${domain}odata/ProductTypes`;
}
@Injectable()
export class EnquiryService extends Service<IEnquiry>{
    public baseUrl = `${domain}odata/Enquirys`;
}
@Injectable()
export class UserService extends Service<IUser>{
    public baseUrl = `${domain}odata/Users`;
}