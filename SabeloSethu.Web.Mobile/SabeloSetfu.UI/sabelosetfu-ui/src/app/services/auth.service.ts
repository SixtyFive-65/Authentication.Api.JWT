import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment.dev';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  //private apiUrl = environment.appConfigUrl;
  private apiUrl = "http://localhost:6565/api";


  private token: string | null = null;
  public username: string | null = null;

  constructor(
    private http: HttpClient,
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    if (isPlatformBrowser(this.platformId)) {
      this.token = localStorage.getItem('token');
      this.username = localStorage.getItem('username'); 
    }
  }

  login(username: string, password: string): Observable<any> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/auth/login`, { username, password }).pipe(
      tap(response => {
        this.token = response.token;
        this.username = username; 
        if (isPlatformBrowser(this.platformId)) {
          localStorage.setItem('token', this.token);
          localStorage.setItem('username', this.username); 
        }
        this.router.navigate(['/']);
      })
    );
  }

  register(username: string, email: string, mobilenumber: string, password: string,  ): Observable<any> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/auth/register`, { username: username, email: email,  mobilenumber: mobilenumber, password: password }).pipe(
      tap(response => {
        this.token = response.token;
        this.username = username; 
        if (isPlatformBrowser(this.platformId)) {
          localStorage.setItem('token', this.token);
          localStorage.setItem('username', this.username);
        }
        this.router.navigate(['/']);
      })
    );
  }

  logout() {
    this.token = null;
    this.username = null;
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('token');
      localStorage.removeItem('username'); 
    }
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return this.token !== null || localStorage.getItem('token') !== null;
    }
    return false;
  }
}
