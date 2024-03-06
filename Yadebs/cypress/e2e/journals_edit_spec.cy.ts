describe('Booking edit tests', () => {
  beforeEach(() => {
    cy.intercept('GET', '/api/Accounts',{fixture: 'accounts.json'}).as('accounts');
    cy.intercept('GET', '/api/Journals', { fixture: 'journals.json' }).as('journals');
  });

  it('shows edit  booking', () => {
    cy.visit('/journal/list');
    cy.get('[data-cy="booking-add"]').click();
  })
})
