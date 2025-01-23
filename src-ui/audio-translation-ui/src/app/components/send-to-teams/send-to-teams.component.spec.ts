import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SendToTeamsComponent } from './send-to-teams.component';

describe('SendToTeamsComponent', () => {
  let component: SendToTeamsComponent;
  let fixture: ComponentFixture<SendToTeamsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SendToTeamsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SendToTeamsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
