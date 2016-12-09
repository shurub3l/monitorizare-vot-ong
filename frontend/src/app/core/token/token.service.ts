import { Observable, Observer } from 'rxjs/Rx';
import { locale } from 'moment';
import { Injectable } from '@angular/core';
import { JwtHelper } from 'angular2-jwt';

@Injectable()
export class TokenService {
  public tokenKey: string = 'token-id';
  private _token: string = undefined

  private jwtHelper = new JwtHelper();
  private _isRefreshing: boolean;

  tokenStream: Observable<string>;
  observers: Observer<string>[] = [];


  public get isRefreshing() {
    return this._isRefreshing;
  }

  constructor() {
    this._token = localStorage.getItem(this.tokenKey)
    this.setTokenStream();
  }

  setTokenStream() {
    this.tokenStream = Observable.create((obs:Observer<string>) => {
      obs.next(this._token);
      this.observers.push(obs);
      return () => {
        this.observers = this.observers.filter(stateObserver => stateObserver === obs);
      }
    });
  }

  public get token() {
    return this._token;
  }
  public set token(value) {
    this._token = value;
    localStorage.setItem(this.tokenKey, value);
    this.observers.forEach(obs => obs.next(value));
  }
  public isTokenExpired() {
    var token = this.token;
    if (!token) {
      return false;
    }
    return this.jwtHelper.isTokenExpired(token);

  }
}
