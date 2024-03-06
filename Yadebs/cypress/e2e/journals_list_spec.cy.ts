describe('Booking list tests', () => {
  beforeEach(() => {
    cy.intercept('GET', '/api/Accounts',
      {
        fixture: 'accounts.json',
        delay:1000
      }).as('accounts');
    cy.intercept('GET', '/api/Journals', { fixture: 'journals.json' }).as('journals');
  });

  it('shows list off bookings', () => {
    cy.visit('/journal/list');
    cy.wait('@accounts').its('request.url').should('include', 'api/Accounts')
  })
})
