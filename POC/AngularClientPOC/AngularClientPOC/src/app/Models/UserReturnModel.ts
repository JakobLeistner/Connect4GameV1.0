import { SessionModel } from "./SessionModel";


export class UserReturnModel
{
    constructor(foreName:string, surName:string, loginName: string, deviceAccess:boolean, Id: number, session: SessionModel, idleTime: number){
        this.ForeName = foreName;
        this.SurName = surName;
        this.LoginName = loginName;
        this.DeviceAccess = deviceAccess;
        this.Id = Id;
        this.Session = session;
        this.IdleTime = idleTime;
    }
    ForeName: string;
    SurName: string;
    LoginName: string;
    DeviceAccess: boolean;
    Id: number;
    Session: SessionModel;
    IdleTime: number;
}