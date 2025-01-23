import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TranslateTextComponent } from './translate-text.component';

describe('TranslateTextComponent', () => {
  let component: TranslateTextComponent;
  let fixture: ComponentFixture<TranslateTextComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TranslateTextComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TranslateTextComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
