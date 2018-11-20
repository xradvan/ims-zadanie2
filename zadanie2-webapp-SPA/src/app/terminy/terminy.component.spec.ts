/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TerminyComponent } from './terminy.component';

describe('TerminyComponent', () => {
  let component: TerminyComponent;
  let fixture: ComponentFixture<TerminyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TerminyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TerminyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
