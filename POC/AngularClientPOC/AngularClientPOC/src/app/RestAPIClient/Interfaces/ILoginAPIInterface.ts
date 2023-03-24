import { Observable } from "rxjs";
import { LoginModel } from "src/app/Models/LoginModel";
import { UserReturnModel } from "src/app/Models/UserReturnModel";

export interface ILoginAPIInterface{
    
    Login(user:LoginModel):Observable<UserReturnModel>;

    Logout(user:LoginModel):Observable<UserReturnModel>;
}