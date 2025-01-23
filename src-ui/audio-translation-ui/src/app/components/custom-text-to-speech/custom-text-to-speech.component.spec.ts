import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomTextToSpeechComponent } from './custom-text-to-speech.component';

describe('CustomTextToSpeechComponent', () => {
  let component: CustomTextToSpeechComponent;
  let fixture: ComponentFixture<CustomTextToSpeechComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomTextToSpeechComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomTextToSpeechComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
