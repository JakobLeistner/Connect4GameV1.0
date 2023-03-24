import { SessionModel } from "./SessionModel";

export class LoginModel
{
    constructor(userName?:string, password?:string, session?:SessionModel, sessionIsValidRequest?:boolean){
        this.password = password;
        this.userName = userName;
        this.SessionIsValidRequest = sessionIsValidRequest;
        this.Session = session;
    }
    userName: string | undefined;
    password: string | undefined;
    Session: SessionModel | undefined;
    SessionIsValidRequest: boolean | undefined;
}