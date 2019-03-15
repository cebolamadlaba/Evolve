import { Component, EventEmitter, Injector, Output, OnInit, Inject, ViewChild, HostListener, AfterViewInit, ChangeDetectorRef} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { finalize } from 'rxjs/operators';
import { CalculationResults } from './models/CalculationResults';
import { CalculationHeaders } from './models/CalculationHeaders';
import { Observable } from 'rxjs';
import { homeService } from './service/home.service';
import { MdbTablePaginationComponent, MdbTableService } from 'angular-bootstrap-md';
import { ToastrManager } from 'ng6-toastr-notifications';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

      @ViewChild(MdbTablePaginationComponent) mdbTablePagination: MdbTablePaginationComponent;
      saving = false;
      fileToUpload: File = null;
      url: string;
      calculationHeadersRequests: Observable<CalculationHeaders[]>;
      calculationResultsRequests: Observable<CalculationResults[]>;
      calculationResults: CalculationResults[];
      calculationHeaders: CalculationHeaders[];

      elements: any = [];
      previous: any = [];
      headElements = ['Formular', 'Input A', 'Input B', 'InputC', 'Result','Message'];

      firstItemIndex;
      lastItemIndex;

      constructor(
          protected http: HttpClient, 
          @Inject('BASE_URL') baseUrl: string,
          private readonly homeService: homeService,
          private tableService: MdbTableService,
          private cdRef: ChangeDetectorRef,
          public toastr: ToastrManager
      ) {
        this.url = baseUrl + 'Home/Upload';
       
      }

      handleFileInput(file: FileList): void {
        this.fileToUpload = file.item(0);
        this.uploadFile(this.fileToUpload); 
      }

      ngOnInit() {

          this.calculationHeadersList();
       }

     populatePagination():void{

          this.mdbTablePagination.setMaxVisibleItemsNumberTo(5);
          this.firstItemIndex = this.mdbTablePagination.firstItemIndex;
          this.lastItemIndex = this.mdbTablePagination.lastItemIndex;

          this.mdbTablePagination.calculateFirstItemIndex();
          this.mdbTablePagination.calculateLastItemIndex();
          this.cdRef.detectChanges();
      }

      calculationHeadersList(): void{

        this.homeService.getCalculationHeaders()
             .subscribe(results => {
                this.calculationHeadersRequests = results;
          });
       }

      calculationResultsList(headerId: number): void{

          this.homeService.getcalculationResultsById(headerId)
            .subscribe(results => {
              this.calculationResultsRequests = results;
              this.tableService.setDataSource(this.calculationResultsRequests);
              this.elements = this.tableService.getDataSource();
              this.previous = this.tableService.getDataSource();
              this.populatePagination();
          });

       }

      uploadFile(fileToUpload): void {

          this.saving = true;

          const formData = new FormData();
          formData.append('file', fileToUpload, fileToUpload.name);
       
          this.http
              .post(this.url, formData)
              .pipe(
                finalize(() => {
              
                this.toastr.infoToastr('Uploading file.' + fileToUpload.name, 'Info');
                })
                ).subscribe(result => {
                  this.saving = false;
                  this.toastr.successToastr('Successfuly uploaded' + fileToUpload.name, 'Success');
                  this.calculationHeadersList();
                  this.calculationResultsList(1);
                },
            error => {
              this.toastr.warningToastr('Fail to upload', 'Failed');
              this.calculationHeadersList();
             
           });
      }

     onNextPageClick(data: any) {
        this.firstItemIndex = data.first;
        this.lastItemIndex = data.last;
     }

     onPreviousPageClick(data: any) {
        this.firstItemIndex = data.first;
        this.lastItemIndex = data.last;
     }

}



