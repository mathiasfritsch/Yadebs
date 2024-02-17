describe('My First Test', () => {

  beforeEach(() => {
    cy.intercept('GET', '/api/Accounts', { fixture: 'accounts.json' }).as('accounts');
  });

  it('Visits the initial project page', () => {
    cy.visit('/accounts/list')

  })
})
