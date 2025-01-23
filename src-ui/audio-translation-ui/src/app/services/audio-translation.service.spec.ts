import { TestBed } from '@angular/core/testing';

import { AudioTranslationService } from './audio-translation.service';

describe('AudioTranslationService', () => {
  let service: AudioTranslationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AudioTranslationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
