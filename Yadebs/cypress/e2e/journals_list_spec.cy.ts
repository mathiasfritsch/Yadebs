describe('Account list tests', () => {

  beforeEach(() => {
    cy.intercept('GET', '/api/Accounts', { fixture: 'accounts.json' }).as('accounts');
    cy.intercept('GET', '/api/Journals', { fixture: 'journals.json' }).as('journals');
  });

  it('shows list off accounts', () => {
    cy.visit('/journal/list');

  })
})
