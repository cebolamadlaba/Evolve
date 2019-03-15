import { Component, EventEmitter, Injector, Output, OnInit, Inject, } from '@angular/core';
import { Observable } from 'rxjs';
import { Response, Headers, RequestOptions } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { CalculationHeaders } from '../models/CalculationHeaders';
import { CalculationResults } from '../models/CalculationResults';
import { map, catchError } from 'rxjs/operators';

export class homeService {

  url: string;
  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl
  }

  getCalculationHeaders() {
    this.url = this.baseUrl + 'Home/GetExistingHeaders';
    return this.http.get(this.url).pipe(map(this.extractData));
  }

  getcalculationResultsById(headerId) {
    this.url = this.baseUrl + 'Home/CalculationResultsById?headerId=' + headerId;
    return this.http.get(this.url).pipe(map(this.extractResultsData));
  }

  private extractData(response: Observable<CalculationHeaders[]>) {
     let body = response;
      return body;
  }

  private extractResultsData(response: Observable<CalculationResults[]>) {
    let body = response;
    return body;
  }
}
