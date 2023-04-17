import { Coffee } from './coffee';

describe('Coffee', () => {
  it('should create an instance', () => {
    expect(new Coffee(1,"ceva",3)).toBeTruthy();
  });
});
