import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { LoginModel } from "../Models/LoginModel";
import { UserReturnModel } from "../Models/UserReturnModel";
import { APIUriBuilder } from "./Services/APIUriBuilder";
import { APIHttpRequest } from "./Services/HttpRequest";
import { ILoginAPIInterface } from "./Interfaces/ILoginAPIInterface";

@Injectable({
	providedIn: 'root'
  })
export class LoginAPIInterface implements ILoginAPIInterface {

    constructor(private apiHttpRequest:APIHttpRequest) {
        this._apiUriBuilder = new APIUriBuilder("User");
    }

    private _apiUriBuilder:APIUriBuilder

    public Login(user:LoginModel):Observable<UserReturnModel>{
        return this.apiHttpRequest.Post(user, this._apiUriBuilder.GetAPIUri("", new Map()));
    }

    public Logout(user:LoginModel):Observable<UserReturnModel>{
        return this.apiHttpRequest.Post(user, this._apiUriBuilder.GetAPIUri("", new Map()));
    }

}