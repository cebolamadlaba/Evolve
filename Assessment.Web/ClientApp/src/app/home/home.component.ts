import { Component, EventEmitter, Injector, Output, Inject,} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
   saving = false;
   fileToUpload: File = null;
   url: string;
   
  constructor(
    protected http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.url = baseUrl+'Home/Upload';
    
  }

  handleFileInput(file: FileList): void {
    this.fileToUpload = file.item(0);
    this.uploadFile(this.fileToUpload);  
  }

  uploadFile(fileToUpload): void {

        this.saving = true;

        const formData = new FormData();
        formData.append('file', fileToUpload, fileToUpload.name);
    
        this.http
        .post(this.url, formData)
        .pipe(
          finalize(() => {

          })
          ).subscribe(result => {
            this.saving = false;
           // this.notify.info('Uploaded Successfully');
    
          },
          error => {
           // this.notify.error('Error: ' + error, 'Failed');
          });

    }


}



