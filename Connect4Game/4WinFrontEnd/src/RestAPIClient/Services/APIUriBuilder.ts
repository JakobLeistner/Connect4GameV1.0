import { Component, Injectable } from "@angular/core";

@Injectable()
export class APIUriBuilder {
    constructor(apiController:String)
    {
        this.APIController = apiController;
        // changed from https to http to fix ssl certificate error on server
        // and now changed it back to https
        this.TargetServerURL = "http://localhost:44320/";
    }

    APIController:String;   
    TargetServerURL:String;

    public GetAPIUri(apiTarget:String, parameter:Map<string, object>):String{
        let counter:number = 0;
        let resApiRes:String =  this.TargetServerURL + this.APIController.toString() + "/" + apiTarget.toString();
        for(let param of parameter.keys()){
            let stringParam = String(parameter.get(param));
            if(counter == 0){
                resApiRes += "?" + param + "=" + stringParam;
            }else{
                resApiRes += "&" + param + "=" + stringParam;
            }
            counter++;    
        }
        console.log("Url: " + resApiRes);
        return resApiRes;
    }

}