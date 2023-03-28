import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
	providedIn: 'root'
  })
export class APIHttpRequest {
constructor(private http: HttpClient) { }
    //GET SINGLE
    Get(requestURL:String): Observable<any> {
			return this.http.get<any>(requestURL.toString());
	}
    
    //PUT
    Update(data: Object, requestURL:String) : Observable<any> {
		const dataStr = JSON.stringify(data);
		return this.http.put(requestURL.toString(), dataStr, {
			headers: new HttpHeaders({
				"Content-Type": "application/json",
			}),
		});
	}

    //POST
	Post(data: Object, requestURL:String) : Observable<any> {
		const dataStr = JSON.stringify(data);
		return this.http.post(requestURL.toString(), dataStr, {
			headers: new HttpHeaders({
				"Content-Type": "application/json",
			}),
		});
	}
    //DELETE
	Delete(data: Object, requestURL:String) : Observable<any> {
		const dataStr = JSON.stringify(data);
		return this.http.delete(requestURL.toString(), {
			headers: new HttpHeaders({
				"Content-Type": "application/json",
			} ),
		});
	}
}