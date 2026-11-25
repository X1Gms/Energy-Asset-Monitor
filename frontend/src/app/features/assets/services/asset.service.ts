import {Injectable, inject} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Asset} from '../models/asset.model';

@Injectable({
    providedIn: 'root'
})
export class AssetService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = 'http://localhost:5199/api/assets';

    getAssets(): Observable<Asset[]>{
        return this.http.get<Asset[]>(this.apiUrl);
    }

    getAssetById(id: number):Observable<Asset>{
        return this.http.get<Asset>(`${this.apiUrl}/${id}`);
    }
}