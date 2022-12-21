import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginContainerComponent } from './container/login-container/login-container.component';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginPresenterComponent } from './presenter/login-presenter/login-presenter.component';
import { MaterialModule } from 'src/app/shared/material.module';



@NgModule({
  declarations: [
    LoginContainerComponent,
    LoginPresenterComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    MaterialModule
  ]
})
export class AuthModule { }
