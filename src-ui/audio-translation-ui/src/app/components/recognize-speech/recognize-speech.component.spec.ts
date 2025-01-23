import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecognizeSpeechComponent } from './recognize-speech.component';

describe('RecognizeSpeechComponent', () => {
  let component: RecognizeSpeechComponent;
  let fixture: ComponentFixture<RecognizeSpeechComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecognizeSpeechComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecognizeSpeechComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
